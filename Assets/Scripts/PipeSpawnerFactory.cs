using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PipeSpawnerFactory
    {
        public PipeSpawner Create(GameObject pipe)
        {
            var res = new PipeSpawner(pipe);
            return res;
        }
    }
}
