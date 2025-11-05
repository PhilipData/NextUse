import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Message } from '../_models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private readonly apiUrl = environment.apiUrl + 'Message';
  constructor(private http: HttpClient) { }

  getAll(): Observable<Message[]>{
    return this.http.get<Message[]>(this.apiUrl)
  }

  create(message: Message): Observable<Message> {
    return this.http.post<Message>(this.apiUrl, message);
  }

  update(messageId:number, message: Message): Observable<Message> {
    return this.http.put<Message>(this.apiUrl + '/' + messageId, message);
  }

  findById(messageId: number): Observable<Message> {
    return this.http.get<Message>(this.apiUrl + '/' + messageId);
  }
   // 
   getChatHistory(myProfileId: number, selectedUserId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/history/${myProfileId}/${selectedUserId}`);
  }
}
