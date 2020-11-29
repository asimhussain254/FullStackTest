import { ILanguage } from './../language.model';
import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { LanguageService } from '../language.service';
import { ToastService } from './../../toast.service';

@Component({
  selector: 'app-language-input',
  templateUrl: './language-input.component.html',
  styleUrls: ['./language-input.component.css'],
})
export class LanguageInputComponent implements OnInit {
  @Input() selectedLanguage: ILanguage;
  languageForm: FormGroup;
  constructor(public languageService: LanguageService, private toastService: ToastService) {}

  ngOnInit(): void {
    this.languageForm = new FormGroup({
      title: new FormControl('', [Validators.required]),
    });
    this.reset();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.languageForm) {
      this.languageForm.patchValue(this.selectedLanguage);
    }
  }

  get title() {
    return this.languageForm.get('title');
  }
  onSubmit() {
    // Destructure value and valid properties from form object
    const { value, valid } = this.languageForm;
    if (valid) {
      const existing = this.selectedLanguage.id > 0;
      let subscription;
      if (existing) {
        subscription = this.languageService.updateLanguage(
          this.selectedLanguage.id,
          {
            ...value,
            id: null,
          },
        );
      } else {
        subscription = this.languageService.createLanguage({
          ...value,
        });
      }
      subscription.subscribe(
        () => {
          this.languageService.getLanguages();
          this.toastService.success(`${this.selectedLanguage.title} is updated...`);
          this.reset();
        },
        (err) => {
          console.log(err);
        },
      );
    }
  }
  private reset() {
    this.languageForm.reset();
    this.selectedLanguage = {id:0,title:''} as ILanguage;
  }
}
