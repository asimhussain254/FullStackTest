import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';

import { ILanguage } from '../../languagesdetails/language.model';
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
      title: new FormControl(0, [Validators.required, Validators.min(1)]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      phone: new FormControl('', [
        Validators.required,
        Validators.pattern(/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im),
      ]),
      dateOfBirth: new FormControl('', [
        Validators.required
      ]),
      gender: new FormControl('', [Validators.required]),
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.userForm) {
      this.userForm.patchValue(this.selectedUser);
    }
  }

  get title() {
    return this.userForm.get('title');
  }
  get firstName() {
    return this.userForm.get('firstName');
  }
  get lastName() {
    return this.userForm.get('lastName');
  }
  get email() {
    return this.userForm.get('email');
  }
  get phone() {
    return this.userForm.get('phone');
  }
  get dateOfBirth() {
    return this.userForm.get('dateOfBirth');
  }
  get gender() {
    return this.userForm.get('gender');
  }
  get language() {
    return this.userForm.get('language');
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
          this.userService.getUsers();
          this.toastService.success(`${this.selectedUser.firstName} ${this.selectedUser.lastName} is updated...`);
          this.reset();
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
