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
namespace Boxes.Integration.Process
{
    using System.Collections.Generic;

    /// <summary>
    /// Set the order to process the packages 
    /// </summary>
    public interface IProcessOrder
    {
        /// <summary>
        /// arranges the packages ready to be processed
        /// </summary>
        /// <param name="packages">the lastest, unprocess packages</param>
        /// <returns>the packags in order, ready to be processed</returns>
        IEnumerable<Package> Arrange(IEnumerable<Package> packages);
    }
}