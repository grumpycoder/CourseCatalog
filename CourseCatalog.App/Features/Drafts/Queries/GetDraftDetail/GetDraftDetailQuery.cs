using MediatR;

namespace CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail
{
    public class GetDraftDetailQuery : IRequest<DraftDetailDto>
    {
        public GetDraftDetailQuery(int draftId)
        {
            DraftId = draftId;
        }

        public int DraftId { get; }
    }
}