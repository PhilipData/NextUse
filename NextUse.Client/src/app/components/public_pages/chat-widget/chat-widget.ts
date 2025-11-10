import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Message } from '../../../_models/message';
import { Profile } from '../../../_models/profile';
import { AuthService } from '../../../_services/auth.service';
import { MessageService } from '../../../_services/message.service';
import { ProfileService } from '../../../_services/profile.service';

@Component({
  selector: 'app-chat-widget',
  imports: [CommonModule, FormsModule],
  templateUrl: './chat-widget.html',
  styleUrl: './chat-widget.css',
})
export class ChatWidget {
 isOpen = signal(false);
  messages = signal<Message[]>([]); // Always initialized - Signal connecting to another ? // return to this
  users = signal<Profile[]>([]);
  selectedUserId: number | null = null;
  newMessage = '';
  myProfileId: number | null = null;

  private messageService = inject(MessageService);
  private profileService = inject(ProfileService);
  private authService = inject(AuthService);

  ngOnInit(): void {

    this.loadLoggedInUser();
    this.loadUsers();
  }

  loadLoggedInUser() {
    this.authService.getProfileForUser().subscribe((profile) => {
      if (profile) {
        this.myProfileId = profile.id;
        console.log('Logged-in User ID:', this.myProfileId);
      }
    });
  }

  loadUsers() {
    this.profileService.getAll().subscribe((users) => {
      if (users) {
        this.users.set(users);
        console.log('Users Loaded:', users);
      }
    });
  }

  loadMessages() {
    if (!this.myProfileId || !this.selectedUserId) return;

    this.messageService.getAll().subscribe((messages) => {
      if (messages) {
        console.log('All Messages:', messages);

        const filteredMessages = messages.filter(
          (msg) =>
            (msg.fromProfile?.id == this.myProfileId && msg.toProfile?.id == this.selectedUserId) ||
            (msg.fromProfile?.id == this.selectedUserId && msg.toProfile?.id == this.myProfileId)
        );

        console.log('Filtered Messages:', filteredMessages);
        this.messages.set(filteredMessages); // When message is send to a user it update the the history, sort of
      }
    });
  }




  getSelectedUserName(): string {
    const user = this.users().find(user => user.id === this.selectedUserId);
    return user ? user.name : 'Unknown';
  }

  toggleChat() {

    this.authService.getProfileForUser().subscribe((profile) => {
      if (profile) {
        this.isOpen.set(!this.isOpen());
        console.log('Logged-in User ID:', this.myProfileId);
      }
    });
  }

  sendMessage() {
    if (!this.newMessage.trim() || !this.selectedUserId || !this.myProfileId) return;

    const message: Message = {
      id: 0, // Assigned by backend
      content: this.newMessage.trim(),
      createdAt: new Date().toISOString(),
      fromProfileId: this.myProfileId,
      toProfileId: this.selectedUserId,
    };

    this.messageService.create(message).subscribe((sentMessage) => {
      if (sentMessage) {
        console.log('Message Sent:', sentMessage);
        this.messages.update((msgs) => [...msgs, sentMessage]);
        this.newMessage = '';
      }
    });
  }

  //  Select user and load their messages
  selectUser(userId: number) {
    this.selectedUserId = userId;
    console.log('Selected User:', this.selectedUserId);
    this.loadMessages();
  }


}
