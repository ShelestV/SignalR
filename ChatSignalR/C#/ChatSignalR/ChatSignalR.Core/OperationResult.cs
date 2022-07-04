using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSignalR.Core;

public class OperationResult<TResult>
{
    private OperationResultState state = OperationResultState.Processing;

    private TResult? result;
    private System.Exception? exception;

    public OperationResultState State => this.state;

    public TResult? Result => this.result;
    public Exception? Exception => this.exception;

    public void Done(TResult? result)
    {
        if (this.CanChangeState())
        {
            this.state = OperationResultState.Done;
            this.result = result;
        }
    }

    public void Error(Exception ex)
    {
        if (this.CanChangeState())
        {
            this.state = OperationResultState.Error;
            this.exception = ex;
        }
    }

    public void NotFound()
    {
        if (this.CanChangeState())
        {
            this.state = OperationResultState.NotFound;
        }
    }

    private bool CanChangeState()
    {
        return this.state == OperationResultState.Processing;
    }

    public static OperationResult<TResult> Create()
    {
        return new OperationResult<TResult>();
    }
}
