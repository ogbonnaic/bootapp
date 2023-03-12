﻿using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x =>  MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
           // Attachments = attachments;
        }

        public Message(IEnumerable<string> to, string subject, string content,List<string> attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
