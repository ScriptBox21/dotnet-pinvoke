﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using static PInvoke.Kernel32;

    /// <content>
    /// Desktop-only extension methods for Kernel32.
    /// </content>
    public static partial class Kernel32Extensions
    {
        /// <summary>
        /// Gets the text associated with an <see cref="NTStatus"/>.
        /// </summary>
        /// <param name="status">The error code.</param>
        /// <returns>The error message.</returns>
        public static string GetMessage(this NTStatus status)
        {
            using (var ntdll = LoadLibrary("ntdll.dll"))
            {
                string formattedMessage = FormatMessage(
                    FormatMessageFlags.FORMAT_MESSAGE_FROM_HMODULE,
                    ntdll.DangerousGetHandle(),
                    (int)status,
                    0,
                    null,
                    MaxAllowedBufferSize);
                if (formattedMessage != null)
                {
                    return formattedMessage;
                }
            }

            return $"Unknown NT_STATUS error (0x{status:x8})";
        }
    }
}