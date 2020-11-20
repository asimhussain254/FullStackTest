import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

import { UserService } from '../user.service';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.css'],
})
export class InputFormComponent implements OnInit {
  @ViewChild('form', { static: false }) userForm: NgForm;
  languages = [
    { name: 'Urdu', value: '1', checked: false },
    { name: 'English', value: '2', checked: false },
    { name: 'Arabic', value: '3', checked: false },
  ];
  constructor(private userService: UserService) {}

  ngOnInit() {
    this.resetForm();
  }

  get userData() {
    return this.userService.selectedUser;
  }
  get selectedOptions() {
    // right now: ['1','3']
    return this.languages.filter((opt) => opt.checked).map((opt) => opt.value);
  }
  onSubmit() {
    // Destructure value and valid properties from form object
    const { value, valid } = this.userForm;
    if (valid) {
      const isNew = this.userService.selectedUser.id === 0;
      let subscription;
      if (isNew) {
        subscription = this.userService.createUser({ ...value, language: this.selectedOptions });
      } else {
        subscription = this.userService.updateUser(this.userService.selectedUser.id, {
          ...value,
          id: null,
          language: this.selectedOptions,
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
      language: null,
    };
  }
}
