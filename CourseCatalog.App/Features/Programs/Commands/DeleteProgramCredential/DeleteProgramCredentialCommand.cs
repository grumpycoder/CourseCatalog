﻿using MediatR;

namespace CourseCatalog.App.Features.Programs.Commands.DeleteProgramCredential
{
    public class DeleteProgramCredentialCommand : IRequest
    {
        public int ProgramId { get; set; }
        public int CredentialId { get; set; }

        public DeleteProgramCredentialCommand(int programId, int credentialId)
        {
            ProgramId = programId;
            CredentialId = credentialId;
        }
    }

}