using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ZenoJam.Common
{
    public class Dialogue : IDisposable
    {
        public event Action<string> NamePrinted;
        public event Action<string> LetterPrinted;
        public event Action<bool> SentencePrinting;
        public event Action DialogueEnded;

        private Queue<MonologueData> _sentences = new Queue<MonologueData>();
        private CancellationTokenSource _monologueToken = new CancellationTokenSource();

        private bool _canTypeNextSentence = true;
        private bool _canTypeSentenceAsync = true;
        private bool _isSentenceFinished = false;

        public void Initiate(DialogueProvider provider) 
        {
            _sentences.Clear();

            foreach (var monologue in provider.NextMonologues)
                _sentences.Enqueue(monologue);

            NextSentenceAsync(_monologueToken.Token).Forget();
        }

        public void Dispose() 
        {
            _monologueToken.Dispose();
        }

        public void NextSentence() 
        {
            if (_isSentenceFinished == false)
                _canTypeSentenceAsync = false;
            else
                _canTypeNextSentence = true;
        }

        private async UniTask NextSentenceAsync(CancellationToken token = default) 
        {
            while (_sentences.Count > 0)
            {
                if (token.IsCancellationRequested)
                    return;

                var monologue = _sentences.Dequeue();

                var typeMonologueTask = TypeMonologueAsync(monologue.Sentences, token);

                NamePrinted?.Invoke(monologue.Name);

                await UniTask.WhenAny(AwaitCancellation(token), typeMonologueTask);
            }

            DialogueEnded?.Invoke();
        }

        private async UniTask TypeMonologueAsync(IEnumerable<string> sentences, CancellationToken token = default) 
        {
            foreach (var sentence in sentences)
            {
                if (token.IsCancellationRequested)
                    return;

                _canTypeNextSentence = false;

                var typeSentenceTask = TypeSentenceAsync(sentence, token);
                var awaitPermisionToType = UniTask.WaitUntil(() => _canTypeNextSentence);

                await UniTask.WhenAny(AwaitCancellation(token), UniTask.WhenAll(typeSentenceTask, awaitPermisionToType));
            }
        }

        private async UniTask TypeSentenceAsync(string sentence, CancellationToken token = default) 
        {
            string typedSentence = "";

            _canTypeSentenceAsync = true;
            _isSentenceFinished = false;

            SentencePrinting?.Invoke(true);

            foreach (var letter in sentence.ToCharArray())
            {
                if (token.IsCancellationRequested)
                    return;

                if (_canTypeSentenceAsync == false)
                {
                    typedSentence = sentence;

                    LetterPrinted?.Invoke(typedSentence);

                    break;
                }
                else 
                {
                    typedSentence += letter;

                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                }

                LetterPrinted?.Invoke(typedSentence);
            }

            SentencePrinting?.Invoke(false);
            _isSentenceFinished = true;
        }

        private async UniTask AwaitCancellation(CancellationToken token = default) => 
            await UniTask.WaitUntil(() => token.IsCancellationRequested);
    }
}