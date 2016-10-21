using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Test.Common
{
    [TestClass]
    public class ObservableListTests
    {
        private Action<IEnumerable<string>> _testAction;
        private List<string> _updatedList;
        private bool _onChangeCalled;
        private ObservableList<string> _observableStrings;

        [TestInitialize] 
        public void Init()
        {
            _testAction = list => { _updatedList = list.ToList(); _onChangeCalled = true; };
            _observableStrings = new ObservableList<string>(_testAction);
        }

        [TestMethod]
        public void ObservableList_Add_ChangedCorrectly()
        {
            _observableStrings.Add("item");

            AssertChangedCorrectly();
        }

        [TestMethod]
        public void ObservableList_Remove_ChangedCorrectly()
        {
            _observableStrings.Add("item");
            _onChangeCalled = false;

            _observableStrings.Remove("item");

            AssertChangedCorrectly();
        }

        [TestMethod]
        public void ObservableList_RemoveAt_ChangedCorrectly()
        {
            _observableStrings.Add("item");
            _onChangeCalled = false;

            _observableStrings.RemoveAt(0);

            AssertChangedCorrectly();
        }

        [TestMethod]
        public void ObservableList_SetThroughIndexing_ChangedCorrectly()
        {
            _observableStrings.Add("item");
            _onChangeCalled = false;

            _observableStrings[0] = "the literal devil";

            AssertChangedCorrectly();
        }

        [TestMethod]
        public void ObservableList_Clear_ChangedCorrectly()
        {
            _observableStrings.Add("item");
            _onChangeCalled = false;

            _observableStrings.Clear();

            AssertChangedCorrectly();
        }

        [TestMethod]
        public void ObservableList_Insert_ChangedCorrectly()
        {
            _observableStrings.Insert(0, "item");

            AssertChangedCorrectly();
        }

        private void AssertChangedCorrectly()
        {
            Assert.AreEqual(true, _onChangeCalled);
            AssertCollectionsEqual(_observableStrings, _updatedList);
        }

        private void AssertCollectionsEqual(IList<string> expected, IList<string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
                expected[i].Equals(actual[i]);
        }
    }
}
