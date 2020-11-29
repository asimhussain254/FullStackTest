import { ILanguage } from './language.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-languagesdetails',
  templateUrl: './languagesdetails.component.html',
  styleUrls: ['./languagesdetails.component.css'],
})
export class LanguagesdetailsComponent implements OnInit {
  selectedLanguage: ILanguage;

  constructor() {}

  ngOnInit(): void {}

  onLanguageChange(language: ILanguage) {
    this.selectedLanguage = language;
  }
}
