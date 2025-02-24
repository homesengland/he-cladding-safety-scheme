using HE.Remediation.Core.Services.Location;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup
{
    public class BuildingLookupRequestHandler : IRequestHandler<BuildingLookupRequest, BuildingLookupResponse>
    {
        private readonly IPostCodeLookup _postCodeLookup;
        public BuildingLookupRequestHandler(IPostCodeLookup postCodeLookup)
        {
            _postCodeLookup = postCodeLookup;
        }

        public async Task<BuildingLookupResponse> Handle(BuildingLookupRequest request, CancellationToken cancellationToken)
        {
            var results = await _postCodeLookup.SearchBuildings(request.Postcode);

            return new BuildingLookupResponse(results);
        }
    }
}
