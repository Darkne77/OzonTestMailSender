﻿using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Core.Repositories;

public interface IMessageHistoryRepository
{
    Task Add(SentMessageResult messageResult);
    Task<IEnumerable<SentMessageResult>> GetAll();
}