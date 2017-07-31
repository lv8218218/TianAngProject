using Abp.AutoMapper;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic.Dto;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MyCompanyName.AbpZeroTemplate.TaTeachingSetting.Topic
{
    public class TopicAppService : AbpZeroTemplateAppServiceBase, ITopicAppService
    {
        private readonly IRepository<Topic, long> _topicRepository;      
        public  TopicAppService(IRepository<Topic,long> topicRepository)
        {
            _topicRepository = topicRepository;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateTopic(TopicDto input)
        {
            await _topicRepository.InsertAsync(input.MapTo<Topic>());
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateTopic(TopicDto input)
        {
            await _topicRepository.UpdateAsync(input.MapTo <Topic>());
        }
        public async Task<List<TopicListDto>> GetTopics(GetTopicInput input)
        {
            var topics = _topicRepository.GetAll();
            if (!string.IsNullOrEmpty(input.Name ) )
            {
                topics = topics.Where(c => c.Name == input.Name);
            }
            topics = topics.OrderByDescending(c => c.LastModificationTime);
            var list = await topics.ToListAsync();
            var topicsList = list.MapTo<List<TopicListDto>>();
            return topicsList;
        }
        public async Task<TopicDto> GetTopic(long id)
        {
            var topic = await _topicRepository.GetAsync(id);
            var dto = topic.MapTo<TopicDto>();
            return dto;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async  Task  DeleteTopic(long id)
        {
            var theTopics = await _topicRepository.GetAsync(id);
            await _topicRepository.DeleteAsync(theTopics);
         }     
    }
}
