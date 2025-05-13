//using MVPv5.Core.Models;
//using MVPv5.Domain.Data;
//using MVPv5.Domain.Entities;

//public static class DocumentMapper
//{
//    public static DocumentEntity ToEntity(DocumentModel model)
//    {
//        return new DocumentEntity
//        {
//            Id = 0, // или model.Id
//            UserId = model.UserId,
//            MetadataJson = JsonHelper.Serialize(model.Metadata)
//        };
//    }

//    public static DocumentModel ToModel(DocumentEntity entity)
//    {
//        return new DocumentModel
//        {
//            UserId = entity.UserId,
//            Metadata = string.IsNullOrEmpty(entity.MetadataJson)
//                ? new Dictionary<string, object>()
//                : JsonHelper.Deserialize<Dictionary<string, object>>(entity.MetadataJson)
//        };
//    }
//}
