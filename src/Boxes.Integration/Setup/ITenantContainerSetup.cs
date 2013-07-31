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
namespace Boxes.Integration.Setup
{
    /// <summary>
    /// the tenants container setup
    /// </summary>
    /// <typeparam name="TBuilder">the ioc builder class</typeparam>
    public interface ITenantContainerSetup<TBuilder> : IContainerSetup<TBuilder> { }


    public class TenantContainerSetup<TBuilder> : ContainerSetupBase<TBuilder>, ITenantContainerSetup<TBuilder>
    {
        public TenantContainerSetup(IRegistrationTaskMapper<TBuilder> registrationTaskMapper) : base(registrationTaskMapper)
        {
        }
    }

}