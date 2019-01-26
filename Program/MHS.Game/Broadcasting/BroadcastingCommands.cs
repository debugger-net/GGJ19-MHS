using System;
using System.Collections.Generic;
using System.Text;

namespace MHS.Game.Broadcasting
{
    /// <summary>
    /// Base class for broadcasting command
    /// </summary>
    public abstract class BroadcastingCommand : Core.LogicCommand
    {
        /// <summary>
        /// Coarse-level type of the command
        /// </summary>
        public override CommandCoarseType CoarseType { get { return CommandCoarseType.kBroadcasting; } }
        
        /// <summary>
        /// Do command in the given world
        /// </summary>
        /// <param name="targetWorld">The world to apply command</param>
        /// <returns>Command processing result</returns>
        internal abstract Core.CommandResult DoCommand(World targetWorld);
    }


    /// <summary>
    /// Command to start a broadcasting stream
    /// </summary>
    public class StartBroadcastingCommand : BroadcastingCommand
    {
        /// <summary>
        /// Construct a broadcasting stream start command with given contents
        /// </summary>
        /// <param name="contents">Streaming contents</param>
        public StartBroadcastingCommand(StreamingContents contents)
        {
            Contents = contents;
        }

        /// <summary>
        /// Contents of the stream
        /// </summary>
        public StreamingContents Contents { get; private set; }


        /// <summary>
        /// Do command in the given world
        /// </summary>
        /// <param name="targetWorld">The world to apply command</param>
        /// <returns>Command processing result</returns>
        internal override Core.CommandResult DoCommand(World targetWorld)
        {
            StreamingSession stream = new StreamingSession(Contents);
            targetWorld.MyChannel.StartStreamingSession(stream);

            return Core.CommandResult.SimpleSuccess;
        }
    }


    /// <summary>
    /// Command to finish broadcasting stream
    /// </summary>
    public class FinishBroadcastingCommand : BroadcastingCommand
    {
        /// <summary>
        /// Construct a broadcasting stream finish command
        /// </summary>
        public FinishBroadcastingCommand()
        {
        }


        /// <summary>
        /// Do command in the given world
        /// </summary>
        /// <param name="targetWorld">The world to apply command</param>
        /// <returns>Command processing result</returns>
        internal override Core.CommandResult DoCommand(World targetWorld)
        {
            bool isSuccess = targetWorld.MyChannel.FinishStreamingSession();
            if (!isSuccess)
            {
                return new Core.CommandResult(false, new CommandFailReason_NoPlayingStream());
            }

            return Core.CommandResult.SimpleSuccess;
        }


        /// <summary>
        /// Command processing fail reason for no playing stream
        /// </summary>
        public class CommandFailReason_NoPlayingStream : Core.ICommandFailReason
        {
            /// <summary>
            /// Readable reason message
            /// </summary>
            /// <returns>Message string</returns>
            public override string Why() { return "There is no stream currently playing."; }
        }
    }
}
