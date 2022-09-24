using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using OzonTestMailSender.Core.Models;
using OzonTestMailSender.Core.Repositories;
using OzonTestMailSender.Infrastructure.Models;

namespace OzonTestMailSender.Infrastructure.Repositories;

public class MessageHistoryRepository : IMessageHistoryRepository
{
    private const string EmailMessageTableName = "EmailMessage";

    private readonly string _connectionString;

    public MessageHistoryRepository(IOptions<ConnectionStrings> connectionStrings)
    {
        _connectionString = connectionStrings.Value.OzonTestMailSenderDB;
    }

    public async Task Add(SentMessageResult messageResult, CancellationToken token)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(token);
        
        var cmdText = 
            $@"INSERT INTO main.""{EmailMessageTableName}"" " +
            @"(""Recipient"", ""Subject"", ""Text"", ""CarbonCopyRecipients"", ""Status"") " + 
            "VALUES (@Recipient, @Subject, @Text, @CarbonCopyRecipients, @Status)";

        var queryArguments = new
                             {
                                 Recipient = messageResult.EmailMessage.Recipient,
                                 Subject = messageResult.EmailMessage.Subject,
                                 Text = messageResult.EmailMessage.Text,
                                 CarbonCopyRecipients = messageResult.EmailMessage.CarbonCopyRecipients,
                                 Status = messageResult.Status
                             };
        
        await connection.ExecuteAsync(cmdText, queryArguments);
    }

    public async Task<IEnumerable<SentMessageResult>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var selectAllMessageHistoryQuery = 
            $@"SELECT * FROM main.""{EmailMessageTableName}""";
        
        // await using var cmd = new NpgsqlCommand(selectAllMessageHistoryQuery, connection);
        // await using var reader = await cmd.ExecuteReaderAsync();
        //
        // var messageHistory = new 
        // while (await reader.ReadAsync())
        // {
        //     reader.GetString(0);
        // }

        return (await connection.QueryAsync<EmailMessageDbModel>(selectAllMessageHistoryQuery))
            .Select(m => m.ToDomain());
    }
}