﻿//Copyright(c) 2015 Bret Copeland<bret@atlantisflight.org>
//
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of
//this software and associated documentation files (the "Software"), to deal in 
//the Software without restriction, including without limitation the rights to
//use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//the Software, and to permit persons to whom the Software is furnished to do so, 
//subject to the following conditions:
// 
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
namespace DotNetWorkQueue.JobScheduler
{
    internal class PendingEvent
    {
        public DateTimeOffset ScheduledTime { get; }
        public ScheduledJob Job { get; }
        public int RunId { get; }

        internal PendingEvent(DateTimeOffset scheduledTime, ScheduledJob job, int runId)
        {
            ScheduledTime = scheduledTime;
            Job = job;
            RunId = runId;
        }

        internal bool IsEarlierThan(PendingEvent ev)
        {
            return ScheduledTime < ev.ScheduledTime;
        }

        internal void Run()
        {
            System.Threading.Tasks.Task.Run(() => Job.RunPendingEvent(this));
        }
    }
}