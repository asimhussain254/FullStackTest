import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
// import { ToastrService } from 'ngx-toastr';

import { UserService } from '../user.service';
// import { ToastService } from './../../toast.service';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.css'],
})
export class InputFormComponent implements OnInit {
  @ViewChild('form',{static: true}) userForm: NgForm;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.resetForm();
  }

  get userData() {
    return this.userService.selectedUser;
  }

  onSubmit() {
    // Destructure value and valid properties from form object
    const { value, valid } = this.userForm;
    if (valid) {
      const isNew = this.userService.selectedUser.userID === 0;
      var subscription;
      if (isNew) {
        subscription = this.userService.createUser(value);
      }
      else {
        subscription = this.userService.updateUser(this.userService.selectedUser.userID,
          { ...value, userID: null });
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
      userID: 0,
      firstName: '',
      lastName: '',
      email: '',
      phone: '',
      dateOfBirth: null,
      language: '',
      title: '',
      gender:''
    };
  }
}
