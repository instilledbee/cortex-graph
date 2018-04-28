﻿using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using System;
using works.ei8.Cortex.Graph.Application;

namespace works.ei8.Cortex.Graph.Port.Adapter.Out.Api
{
    public class GraphModule : NancyModule
    {
        public GraphModule(INeuronQueryService queryService) : base("/{avatarId}/cortex/graph/neurons")
        {
            this.Get("/{neuronid:guid}", async (parameters) =>
            {
                var nv = await queryService.GetNeuronDataById(parameters.avatarId, parameters.neuronid);
                return new TextResponse(JsonConvert.SerializeObject(nv));
            }
            );

            this.Get("/{neuronid}/dendrites", async (parameters) =>
            {
                var nv = await queryService.GetAllDendritesById(parameters.avatarId, parameters.neuronid);
                return new TextResponse(JsonConvert.SerializeObject(nv));
            }
            );

            this.Get("/{function}", async (parameters) =>
            {
                object response = null;

                if (parameters.function == "search")
                {
                    response = await queryService.GetAllNeuronsByDataSubstring(parameters.avatarId, this.Request.Query["data"]);
                }

                return new TextResponse(JsonConvert.SerializeObject(response));
            }
            );
        }
    }
}