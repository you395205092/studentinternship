using Exam.DTO;
using Exam.IService;
using Exam.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service
{
    public class MessageService : IMessageService
    {
        public int Add(MessageDTO dto)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                MessageEntity ef = new MessageEntity();
                ef.MesContent = dto.MesContent;
                ef.UserId = dto.UserId;
                ef.Title = dto.Title;
                ctx.Messages.Add(ef);
                ctx.SaveChanges();
                return ef.Id;
            }
        }
        public int Edit(int id, string ReplyContent)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<MessageEntity> bs = new BaseService<MessageEntity>(ctx);
                var data= bs.GetById(id);
                if (data==null)
                {
                    throw new NullReferenceException("找不到该id"+ id);
                }
                data.ReplyContent = ReplyContent;
                data.ReplayTime = DateTime.Now;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public MessageDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<MessageEntity> bs = new BaseService<MessageEntity>(ctx);
                var data = bs.GetAll().ToList();
                return data.Select(e => ToDTO(e)).ToArray();
            }
        }

        public MessageDTO GetById(int Id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<MessageEntity> bs = new BaseService<MessageEntity>(ctx);
                var data = bs.GetById(Id);
                return ToDTO(data);
            }
        }

        public MessageDTO[] GetByUserId(int userId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<MessageEntity> bs = new BaseService<MessageEntity>(ctx);
                var data= bs.GetAll().Where(e => e.UserId == userId).ToList();
                return data.Select(e => ToDTO(e)).ToArray();
            }
        }
        public MessageDTO ToDTO(MessageEntity ef)
        {
            MessageDTO dto = new MessageDTO();
            dto.Id = ef.Id;
            dto.MesContent = ef.MesContent;
            dto.ReplayTime = ef.ReplayTime;
            dto.ReplyContent = ef.ReplyContent;
            dto.Title = ef.Title;
            dto.UserId = ef.UserId;
            dto.CreateDateTime = ef.CreateDateTime;
            return dto;
        }


    }
}
