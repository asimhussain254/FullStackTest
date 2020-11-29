import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ILanguage } from './language.model';
@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  selectedLanguage: ILanguage;
  languageList: ILanguage[];
  constructor(private httpClient: HttpClient) {}
  getLanguages() {
    this.httpClient
      .get('api/Language')
      .subscribe((res) => (this.languageList = res as ILanguage[]));
  }

  createLanguage(language: ILanguage) {
    return this.httpClient.post('api/language', language);
  }

  updateLanguage(languageId: number, language: ILanguage) {
    return this.httpClient.put(`api/language/${languageId}`, language);
  }

  deleteLanguage(languageId: number) {
    return this.httpClient.delete(`api/language/${languageId}`);
  }
}
