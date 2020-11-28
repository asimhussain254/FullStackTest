import { ILanguage } from './language.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IUser } from './user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  userList: IUser[];
  languageList: ILanguage[];
  constructor(private httpClient: HttpClient) {}

  getUsers() {
    this.httpClient.get('api/User').subscribe((res) => (this.userList = res as IUser[]));
  }
  getLanguages() {
    this.httpClient
      .get('api/Language')
      .subscribe((res) => (this.languageList = res as ILanguage[]));
  }

  createUser(user: IUser) {
    return this.httpClient.post('api/User', user);
  }

  updateUser(userId: number, user: IUser) {
    return this.httpClient.put(`api/User/${userId}`, user);
  }

  deleteUser(userId: number) {
    return this.httpClient.delete(`api/User/${userId}`);
  }
}
