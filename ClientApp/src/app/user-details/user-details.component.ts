import { Component, OnInit } from '@angular/core';
import { IUser } from './user.model';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent implements OnInit {
  selectedUser: Partial<IUser> = { title: 0, languages: [] };

  constructor() {}

  ngOnInit() {}

  onUserChange(user: IUser) {
    this.selectedUser = user;
  }
}
