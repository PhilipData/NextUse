import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './components/navbar/navbar';
import { ChatWidget } from './components/public_pages/chat-widget/chat-widget';
import { CommonModule } from '@angular/common';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar,ChatWidget,CommonModule ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('NextUse.Client');
   constructor(private auth: AuthService) { }


  
  ngOnInit() {
  }

  isAuthenticated():Boolean {
    return this.auth.isAuthenticated()
  }
}
