// Copyright (c) 2012, Event Store LLP
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
// 
// Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
// Redistributions in binary form must reproduce the above copyright
// notice, this list of conditions and the following disclaimer in the
// documentation and/or other materials provided with the distribution.
// Neither the name of the Event Store LLP nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.Collections.Generic;
using System.Text;
using EventStore.Projections.Core.Services.Processing;
using NUnit.Framework;

namespace EventStore.Projections.Core.Tests.Other
{
    [TestFixture]
    class can_serialize_and_deserialize
    {
        [Test]
        public void position_based_checkpoint_tag()
        {
            CheckpointTag tag = CheckpointTag.FromPosition(-1, 0);
            byte[] bytes = tag.ToJsonBytes();
            string instring = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(instring);

            var back = instring.ParseJson<CheckpointTag>();
            Assert.AreEqual(tag, back);
        }

        [Test]
        public void prepare_position_based_checkpoint_tag_zero()
        {
            CheckpointTag tag = CheckpointTag.FromPreparePosition(0);
            byte[] bytes = tag.ToJsonBytes();
            string instring = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(instring);

            var back = instring.ParseJson<CheckpointTag>();
            Assert.AreEqual(tag, back);
        }

        [Test]
        public void prepare_position_based_checkpoint_tag_minus_one()
        {
            CheckpointTag tag = CheckpointTag.FromPreparePosition(-1);
            byte[] bytes = tag.ToJsonBytes();
            string instring = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(instring);

            var back = instring.ParseJson<CheckpointTag>();
            Assert.AreEqual(tag, back);
        }

        [Test]
        public void prepare_position_based_checkpoint_tag()
        {
            CheckpointTag tag = CheckpointTag.FromPreparePosition(1234);
            byte[] bytes = tag.ToJsonBytes();
            string instring = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(instring);

            var back = instring.ParseJson<CheckpointTag>();
            Assert.AreEqual(tag, back);
        }

        [Test]
        public void stream_based_checkpoint_tag()
        {
            CheckpointTag tag = CheckpointTag.FromStreamPosition("$ce-account", 12345);
            byte[] bytes = tag.ToJsonBytes();
            string instring = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(instring);

            var back = instring.ParseJson<CheckpointTag>();
            Assert.AreEqual(tag, back);
            Assert.IsNull(back.CommitPosition);
        }

        [Test]
        public void streams_based_checkpoint_tag()
        {
            CheckpointTag tag = CheckpointTag.FromStreamPositions(new Dictionary<string, int>{{"a", 1}, {"b", 2}});
            byte[] bytes = tag.ToJsonBytes();
            string instring = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(instring);

            var back = instring.ParseJson<CheckpointTag>();
            Assert.AreEqual(tag, back);
            Assert.IsNull(back.CommitPosition);
        }
    }
}
