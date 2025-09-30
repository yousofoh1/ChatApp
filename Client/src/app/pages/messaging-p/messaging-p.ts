import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-messaging-p',
  imports: [DatePipe],
  templateUrl: './messaging-p.html',
  styleUrl: './messaging-p.scss',
})
export class MessagingP {
  messageHistory: { incoming: boolean; message: string; date: Date }[] = [
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
    {
      incoming: true,
      message: 'Hello, this is a message from Mohamemed Alsayed',
      date: new Date(),
    },
    {
      incoming: false,
      message: 'Hello, this is a message from you',
      date: new Date(),
    },
  ];
}
