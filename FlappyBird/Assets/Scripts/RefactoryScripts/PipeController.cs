using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController
{
    PipeSpawner pipeSpawner;

    Pipe curPipe;

    CachedQueue<Pipe> PipeQueue;

    public PipeController()
    {
        PipeQueue = new CachedQueue<Pipe>(3);
    }
}
