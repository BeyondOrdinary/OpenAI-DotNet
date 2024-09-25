// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using NUnit.Framework;
using System.Text.Json;
using OpenAI.Assistants;
using OpenAI.Threads;

namespace OpenAI.Tests
{
    public class SerializationTests
    {
        [Test]
        public void TestListResponse()
        {
            string json = @"{
  ""object"": ""list"",
  ""data"": [
    {
      ""id"": ""msg_C68foC1B4Elo1oYr6A6qZdqU"",
      ""object"": ""thread.message"",
      ""created_at"": 1727305136,
      ""assistant_id"": ""asst_000000000000000000000000"",
      ""thread_id"": ""thread_000000000000000000000000"",
      ""run_id"": ""run_000000000000000000000000"",
      ""role"": ""assistant"",
      ""content"": [
        {
          ""type"": ""text"",
          ""text"": {
            ""value"": ""The response."",
            ""annotations"": [
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:0+source?"",
                ""start_index"": 4104,
                ""end_index"": 4116,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000008""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:1+source?"",
                ""start_index"": 4116,
                ""end_index"": 4128,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000007""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:2+source?"",
                ""start_index"": 4128,
                ""end_index"": 4140,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000006""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:3+source?"",
                ""start_index"": 4140,
                ""end_index"": 4152,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000009""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:4+source?"",
                ""start_index"": 4152,
                ""end_index"": 4164,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000010""
                }
              }
            ]
          }
        }
      ],
      ""attachments"": [],
      ""metadata"": {}
    },
    {
      ""id"": ""msg_objx0pgECwxSURBJ0f6p09m8"",
      ""object"": ""thread.message"",
      ""created_at"": 1727302439,
      ""assistant_id"": ""asst_000000000000000000000000"",
      ""thread_id"": ""thread_000000000000000000000000"",
      ""run_id"": ""run_000000000000000000000000"",
      ""role"": ""assistant"",
      ""content"": [
        {
          ""type"": ""text"",
          ""text"": {
            ""value"": ""The Response"",
            ""annotations"": [
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:0+source?"",
                ""start_index"": 4104,
                ""end_index"": 4116,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000000""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:1+source?"",
                ""start_index"": 4116,
                ""end_index"": 4128,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000001""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:2+source?"",
                ""start_index"": 4128,
                ""end_index"": 4140,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000002""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:3+source?"",
                ""start_index"": 4140,
                ""end_index"": 4152,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000003""
                }
              },
              {
                ""type"": ""file_citation"",
                ""text"": ""?4:4+source?"",
                ""start_index"": 4152,
                ""end_index"": 4164,
                ""file_citation"": {
                  ""file_id"": ""file-000000000000000000000004""
                }
              }
            ]
          }
        }
      ],
      ""attachments"": [],
      ""metadata"": {}
    },
    {
      ""id"": ""msg_OHYpkdfTCqsjGSpNep5BIEPx"",
      ""object"": ""thread.message"",
      ""created_at"": 1727302436,
      ""assistant_id"": null,
      ""thread_id"": ""thread_000000000000000000000000"",
      ""run_id"": null,
      ""role"": ""user"",
      ""content"": [
        {
          ""type"": ""text"",
          ""text"": {
            ""value"": ""A value"",
            ""annotations"": []
          }
        }
      ],
      ""attachments"": [],
      ""metadata"": {}
    }
  ],
  ""first_id"": ""msg_C68foC1B4Elo1oYr6A6qZdqU"",
  ""last_id"": ""msg_OHYpkdfTCqsjGSpNep5BIEPx"",
  ""has_more"": false
}";
            var foo = JsonSerializer.Deserialize(json, typeof(ListResponse<MessageResponse>), OpenAIClient.JsonSerializationOptions);
            Assert.NotNull(foo);
        }
        [Test]
        public void TestDeletedResponse()
        {
            string json = @"{
              ""id"": ""thread_JX7HQYXYv16FnzqhgW3L2Uq6"",
              ""object"": ""thread.deleted"",
              ""deleted"": true
            }";
            var foo = JsonSerializer.Deserialize(json, typeof(DeletedResponse), OpenAIClient.JsonSerializationOptions);
            Assert.NotNull(foo);

        }
        [Test]
        public void TestThreadRunResponse()
        {
            string json = @"{
                  ""id"": ""run_mpG1J77WtH72wEll4uwZjWEP"",
                  ""object"": ""thread.run"",
                  ""created_at"": 1727305133,
                  ""assistant_id"": ""asst_MbMMhqYYQyJb3gizxwxtHfvq"",
                  ""thread_id"": ""thread_Rq25GIeHOlaISMKZPZBNbIsr"",
                  ""status"": ""completed"",
                  ""started_at"": 1727305134,
                  ""expires_at"": null,
                  ""cancelled_at"": null,
                  ""failed_at"": null,
                  ""completed_at"": 1727305141,
                  ""required_action"": null,
                  ""last_error"": null,
                  ""model"": ""gpt-4o"",
                  ""instructions"": ""These are the instructions. Read me."",
                  ""tools"": [
                    {
                      ""type"": ""file_search"",
                      ""file_search"": {}
                    }
                  ],
                  ""tool_resources"": {},
                  ""metadata"": {},
                  ""temperature"": 0.37,
                  ""top_p"": 0.2,
                  ""max_completion_tokens"": null,
                  ""max_prompt_tokens"": null,
                  ""truncation_strategy"": {
                    ""type"": ""auto"",
                    ""last_messages"": null
                  },
                  ""incomplete_details"": null,
                  ""usage"": {
                    ""prompt_tokens"": 17694,
                    ""completion_tokens"": 229,
                    ""total_tokens"": 17923
                  },
                  ""response_format"": {
                    ""type"": ""text""
                  },
                  ""tool_choice"": ""auto"",
                  ""parallel_tool_calls"": true
                }";
            var foo = JsonSerializer.Deserialize(json, typeof(RunResponse), OpenAIClient.JsonSerializationOptions);
            Assert.NotNull(foo);    
        }
        [Test]
        public void TestThreadResponse()
        {
            string json = @"{
                  ""id"": ""thread_PvptSMFBkB6v3Fp58cFLBqmq"",
                  ""object"": ""thread"",
                  ""created_at"": 1727302436,
                  ""metadata"": {},
                  ""tool_resources"": {}
                }";
            var foo = JsonSerializer.Deserialize(json, typeof(ThreadResponse), OpenAIClient.JsonSerializationOptions);
            Assert.NotNull(foo);
        }
        [Test]
        public void TestAutoResponseFormatAssistantResponse()
        {
            string json = @"{
                  ""id"": ""asst_hvfNM8KxLRvvT9G2S9H3gtwK"",
                  ""object"": ""assistant"",
                  ""created_at"": 1722632806,
                  ""name"": ""DEV_Solicitation_FA9453-23-R-A004"",
                  ""description"": ""The ranking assistant for soliciation FA9453-23-R-A004"",
                  ""model"": ""gpt-4o"",
                  ""instructions"": ""These are the instructions. Read me."",
                  ""tools"": [
                    {
                      ""type"": ""file_search"",
                      ""file_search"": {}
                    }
                  ],
                  ""top_p"": 0.2,
                  ""temperature"": 0.37,
                  ""tool_resources"": {
                    ""file_search"": {
                      ""vector_store_ids"": [
                        ""vs_gCJXj4LHohIKF2RLEJGsmi06""
                      ]
                    }
                  },
                  ""metadata"": {},
                  ""response_format"": ""auto""
                }";
            var foo = JsonSerializer.Deserialize(json, typeof(AssistantResponse), OpenAIClient.JsonSerializationOptions);
            Assert.NotNull(foo);
        }
        [Test]
        public void TestTextResponseFormatAssistantResponse()
        {
            string json = @"{
                  ""id"": ""asst_IY3hnNHbNkneotffWgaRl69W"",
                  ""object"": ""assistant"",
                  ""created_at"": 1724861806,
                  ""name"": ""DEV_Solicitation_80NSSC24881759Q"",
                  ""description"": ""The ranking assistant for soliciation 80NSSC24881759Q"",
                  ""model"": ""gpt-4o"",
                  ""instructions"": ""These are the instructions. Read me."",
                  ""tools"": [
                    {
                      ""type"": ""file_search"",
                      ""file_search"": {}
                    }
                  ],
                  ""top_p"": 0.2,
                  ""temperature"": 0.37,
                  ""tool_resources"": {
                    ""file_search"": {
                      ""vector_store_ids"": [
                        ""vs_natjFxWvAE5xYd1Nwjteg4E6""
                      ]
                    }
                  },
                  ""metadata"": {},
                  ""response_format"": {
                    ""type"": ""text""
                  }
                }";
            var foo = JsonSerializer.Deserialize(json, typeof(AssistantResponse), OpenAIClient.JsonSerializationOptions);
            Assert.NotNull(foo);
        }
    }
}
