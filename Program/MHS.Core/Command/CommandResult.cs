using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Core
{
    /// <summary>
    /// Result of game logic command
    /// </summary>
    public class CommandResult
    {
        public readonly bool isSuccess;

        public readonly ICommandResultData payloadData;
        public readonly ICommandFailReason failReason;


        public CommandResult(bool isSuccessResult,
            ICommandFailReason failedReason = null, ICommandResultData resultData = null)
        {
            isSuccess = isSuccessResult;
            failReason = failedReason;
            payloadData = resultData;
        }

        /// <summary>
        /// Success result without data
        /// </summary>
        public static CommandResult SimpleSuccess { get { return ms_simpleSuccess; } }
        private static CommandResult ms_simpleSuccess;

        static CommandResult()
        {
            ms_simpleSuccess = new CommandResult(true);
        }
    }


    /// <summary>
    /// Interface for command result additional data
    /// </summary>
    public interface ICommandResultData
    {
    }

    /// <summary>
    /// Interface for detailed command fail reason
    /// </summary>
    public abstract class ICommandFailReason
    {
        /// <summary>
        /// Readable reason message
        /// </summary>
        /// <returns>Message string</returns>
        public abstract string Why();

        /// <summary>
        /// Cause of the reason
        /// </summary>
        public ICommandFailReason Cause { get { return null; } }
    }
}
