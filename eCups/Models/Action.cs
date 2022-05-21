using System;
using System.Threading.Tasks;
using eCups.Helpers;

namespace eCups.Models
{
    public class Action
    {
        public string Name;
        public string Event;
        public int Id;
        public bool BlocksInput;
        public int Priority;
        public int TargetPageId { get; set; }
        public string Param;

        public Action(int id, int targetPageId)
        {
            this.Id = id;
            this.Name = null;
            this.Event = null;
            this.BlocksInput = false;
            this.Priority = (int)Actions.PriorityName.Low;
            this.TargetPageId = targetPageId;
            this.Param = null;
        }

        public Action(int id, string param)
        {
            this.Id = id;
            this.Name = null;
            this.Event = null;
            this.BlocksInput = false;
            this.Priority = (int)Actions.PriorityName.Low;
            this.TargetPageId = -1;
            this.Param = param;
        }

        public Action(int id)
        {
            this.Id = id;
            this.Name = null;
            this.Event = null;
            this.BlocksInput = false;
            this.Priority = (int)Actions.PriorityName.Low;
            this.TargetPageId = -1;
            this.Param = null;
        }

        public async Task<bool> Execute()
        {
            await App.PerformActionAsync(this);
            return true;
        }
    }
}
