// Copyright 2012 - 2013 dbones.co.uk (David Rundle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace Boxes.Integration.Tasks
{
    using System.Collections.Generic;
    using Boxes.Tasks;

    public abstract class RunOnceBoxesTask<T> : IBoxesTask<T>
    {
        private readonly ICollection<T> _handled = new HashSet<T>();

        public bool CanHandle(T item)
        {
            return CanHandleItem(item) && !_handled.Contains(item);
        }

        protected abstract bool CanHandleItem(T item);

        public void Execute(T item)
        {
            ExecuteOnItem(item);
            _handled.Add(item);
        }

        protected abstract void ExecuteOnItem(T item);
    }
}