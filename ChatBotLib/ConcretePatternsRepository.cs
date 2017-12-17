﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using ChatBotLib.Entities;
using SamplesToTextsMatcher;
using System.Linq;

namespace ChatBotLib
{
    public class ConcretePatternsRepository : AbstractPatternsRepository
    {
        private ContextsIndex _index;
        private string _connectionString;
        private ChatBotContext _chatBotContext;
        //private Logger

        public override void Init()
        {
            _chatBotContext = new ChatBotContext();
            _contexts = _chatBotContext.Contexts.ToList<Context>();
            _index = new ContextsIndex(_contexts);
        }

        public override bool AddContext(Context context)
        {
            try
            {
                _chatBotContext.Contexts.Add(context);
                if (_index.AddContext(context))
                    return true;
            }
            catch(Exception ex)
            {

            }

            return false;
        }

        public override bool DeleteContext(Context context)
        {
            _index.DeleteContext(context);
            throw new NotImplementedException();
        }

        public override bool UpdateContext(Context context)
        {
            _index.UpdateContext(context);
            throw new NotImplementedException();
        }

        public override List<Context> GetActualContexts(TheProject prj, string[] terms)
        {
            return _index.GetContextsForTerms(terms, prj);
        }

        public override void AddBotResponseToPattern(Context context, BotResponse botResponse)
        {
            throw new NotImplementedException();
        }

        public override void DeleteBotResponseToPattern(Context context, BotResponse botResponse)
        {
            throw new NotImplementedException();
        }

        public override List<BotResponse> GetBotResponsesToPattern(Context context)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBotResponseToPattern(Context context, BotResponse botResponse)
        {
            throw new NotImplementedException();
        }
    }
}
