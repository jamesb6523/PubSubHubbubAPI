﻿using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace PubSubHubbubAPI.Data
{
    public static class MongoDBOperations
    {
        public static void InsertToMongoDB(string message)
        {
            string connection = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

            var client = new MongoClient(connection);
            var database = client.GetDatabase("pubsubhubbub");
            var countersCollection = database.GetCollection<BsonDocument>("counters");
            var filter = Builders<BsonDocument>.Filter.Eq("id", "MessageId");
            var update = Builders<BsonDocument>.Update.Inc("seq", 1);
            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                ReturnDocument = ReturnDocument.After
            };
            var sequenceDocument = countersCollection.FindOneAndUpdate(filter, update, options);
            var nextSequenceValue = sequenceDocument["seq"].AsInt32;
            var document = new BsonDocument
            {
                { "InboundMessageId", nextSequenceValue },
                { "MessageText", message },
                { "DateCreated", DateTime.Now.ToString() },
                { "Processed", "0" },
                { "Investigate", "0" },
                { "AlreadyExists", "0" },
                { "Priority", "0" },
            };
            var collection = database.GetCollection<BsonDocument>("InboundMessages");
            collection.InsertOne(document);
        }
    }
}
