import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IUser } from './user.model';

@Injectable()
export class UserService {
  selectedUser: IUser;
  userList: IUser[];
  constructor(private httpClient: HttpClient) {}

  getUsers() {
    this.httpClient.get('https://localhost:5001/api/User').subscribe((res) => (this.userList = res as IUser[]));
  }

  createUser(user: IUser) {
    return this.httpClient.post('https://localhost:5001/api/User', user);
  }

  updateUser(userId: number, user: IUser) {
    return this.httpClient.put(`https://localhost:5001/api/User/${userId}`, user);
  }
  deleteUser(userId: number){
    return this.httpClient.delete(`https://localhost:5001/api/User/${userId}`);
  }
}
