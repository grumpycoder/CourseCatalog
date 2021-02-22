using MediatR;

namespace CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail
{
    public class GetDraftDetailQuery : IRequest<DraftDetailDto>
    {
        public int DraftId { get; }

        public GetDraftDetailQuery(int draftId)
        {
            DraftId = draftId;
        }
    }
}
