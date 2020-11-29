import { ILanguage } from './../language.model';
import { LanguageService } from './../language.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-language-list',
  templateUrl: './language-list.component.html',
  styleUrls: ['./language-list.component.css'],
})
export class LanguageListComponent implements OnInit {
  @Output() languageChanged = new EventEmitter<ILanguage>();
  constructor(private languageService: LanguageService) {}

  ngOnInit() {
    this.languageService.getLanguages();
  }

  get lanuages() {
    return this.languageService.languageList;
  }
  deleteUser(language: ILanguage) {
    if (confirm(`'Are you sure you want to delete the ${language.title} language?'`)) {
      this.languageService.deleteLanguage(language.id).subscribe(
        (res) => {
          this.languageService.getLanguages();
        },
        (err) => {
          console.log(err);
        },
      );
    }
  }
}
