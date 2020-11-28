import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';

import { ILanguage } from '../language.model';
import { ToastService } from './../../toast.service';
import { UserService } from '../user.service';
import { IUser } from '../user.model';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.css'],
})
export class InputFormComponent implements OnInit, OnChanges {
  @Input() selectedUser: IUser;

  userForm: FormGroup;
  constructor(public userService: UserService, private toastService: ToastService) {}

  ngOnInit() {
    this.userService.getLanguages();
    this.userForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', [
        Validators.required,
        // Validators.pattern(
        //   '(^+[0-9]{2}|^+[0-9]{2}(0)|^(+[0-9]{2})(0)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9-s]{10}$)',
        // ),
      ]),
      dateOfBirth: new FormControl(null, [Validators.required]),
      gender: new FormControl(null, [Validators.required]),
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.userForm) {
      this.userForm.patchValue(this.selectedUser);
    }
  }

  getLName() {
    return this.userForm.get('lasttName');
  }
  getEmail() {
    return this.userForm.get('email');
  }
  getPhone() {
    return this.userForm.get('phoneNumber');
  }
  getDOB() {
    return this.userForm.get('dateOfBirth');
  }

  get languages() {
    return { ...this.userService.languageList };
  }

  getLanguageModel(langualge: ILanguage) {
    return this.selectedUser && this.selectedUser.languages.find((x) => x === langualge.id);
  }

  onLanguageChange(checked, language: ILanguage) {
    if (checked) {
      this.selectedUser.languages.push(language.id);
    } else {
      this.selectedUser.languages = this.selectedUser.languages.filter((l) => l !== language.id);
    }
  }

  onSubmit() {
    // Destructure value and valid properties from form object
    const { value, valid } = this.userForm;
    if (valid) {
      const existing = this.selectedUser.id > 0;
      let subscription;
      if (existing) {
        subscription = this.userService.updateUser(this.selectedUser.id, {
          ...value,
          id: null,
          languages: this.selectedUser.languages,
        });
      } else {
        subscription = this.userService.createUser({
          ...value,
          languages: this.selectedUser.languages,
        });
      }
      subscription.subscribe(
        () => {
          this.reset();
          this.userService.getUsers();
          this.toastService.success('I am a success toast');
        },
        (err) => {
          console.log(err);
        },
      );
    }
  }

  private reset() {
    this.userForm.reset();
    this.selectedUser = { title: 0, languages: [] } as IUser;
  }
}
