using System;

namespace SecurityConsultantCore.Common
{
    public class ConditionalAction
    {
        private Func<bool> _condition;
        private Action _onTrueAction;
        private Action _onFalseAction;

        public ConditionalAction(Func<bool> condition, Action onTrueAction, Action onFalseAction)
        {
            _condition = condition;
            _onTrueAction = onTrueAction;
            _onFalseAction = onFalseAction;
        }

        public void Invoke()
        {
            if (_condition.Invoke())
                _onTrueAction.Invoke();
            else
                _onFalseAction.Invoke();
        }
    }
}
