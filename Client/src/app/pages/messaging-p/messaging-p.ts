import { DatePipe } from '@angular/common';
import { Component, input } from '@angular/core';
import { InputText } from 'primeng/inputtext';
import { InputGroup } from 'primeng/inputgroup';
import { Button } from 'primeng/button';
import { BaseComp } from '../../components/BaseComp';
import { IChatMessage } from '../../services/signalr/signal-r-s';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-messaging-p',
  imports: [DatePipe, InputText, Button, InputGroup],
  templateUrl: './messaging-p.html',
  styleUrl: './messaging-p.scss',
})
export class MessagingP extends BaseComp {
  userName = input.required<string>();
  user = this.authS.user();

  messageHistory: { incoming: boolean; message: string; date: Date }[] = [];

  ngOnInit() {
    this.signalRS.messages$.subscribe((message) => {
      if (message) {
        this.messageHistory.push({
          incoming: true,
          message: message.text,
          date: new Date(),
        });
      }
    });
  }

  sendMessage(event: Event,text: HTMLInputElement) {
    if (!text.value) {
      return;
    }
    if(event instanceof KeyboardEvent ) {
      if(event.key !== 'Enter') {
        return;
      }
    }

    let message: IChatMessage = {
      receiverUserName: this.userName(),
      senderFullName: '',
      senderUserName: this.user?.userName || '',
      senderImageUrl: '',
      text: text.value,
    };

    this.messageHistory.push({
      incoming: false,
      message: message.text,
      date: new Date(),
    });

    this.signalRS.sendMessage(message);

    text.value = '';
  }
}
