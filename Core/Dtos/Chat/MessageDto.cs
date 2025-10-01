using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Chat;

public record MessageDto(
        string SenderUserName,
        string SenderImageUrl,
        string SenderFullName,
        string ReceiverUserName,
        string Text
    );
