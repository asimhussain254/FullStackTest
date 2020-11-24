import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ILanguage } from '../language.model';

import { UserService } from '../user.service';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.css'],
})
export class InputFormComponent implements OnInit {
  @ViewChild('form', { static: true }) userForm: NgForm;
  constructor(public userService: UserService) {}

  ngOnInit() {
    this.userService.getLanguages();
    this.resetForm();
  }

  get userData() {
    return this.userService.selectedUser;
  }
  get languages() {
    return { ...this.userService.languageList };
  }

  getLanguageModel(langualge: ILanguage) {
    return this.userData.languages.find((x) => x === langualge.id);
  }

  onLanguageChange(checked, language: ILanguage) {
    if (checked) {
      this.userData.languages.push(language.id);
    } else {
      this.userData.languages = this.userData.languages.filter((l) => l !== language.id);
    }
  }
  updateDate(date:Date){
      this.userData.dateOfBirth = date;
  }
  onSubmit() {
    // Destructure value and valid properties from form object
    const { value, valid } = this.userForm;
    if (valid) {
      const isNew = this.userService.selectedUser.id === 0;
      let subscription;
      if (isNew) {
        subscription = this.userService.createUser({
          ...value,
          languages:this.userData.languages
        });
      } else {
        subscription = this.userService.updateUser(this.userService.selectedUser.id, {
          ...value,
          id: null,
          languages:this.userData.languages
        });
      }
      subscription.subscribe(
        (res) => {
          this.resetForm();
          this.userService.getUsers();
          // this.toastr.success(`Record is updated.`,"Success")
        },
        (err) => {
          console.log(err);
        },
      );
    }
  }

  private resetForm() {
    this.userForm.reset();
    this.userService.selectedUser = {
      id: 0,
      firstName: '',
      lastName: '',
      email: '',
      phone: '',
      dateOfBirth: null,
      title: 0,
      gender: 0,
      languages: [],
      languagesTitles: [],
    };
  }
}
