import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbDateAdapter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomNgbDateNativeUTCAdapter } from './services';
import { QuillModule } from 'ngx-quill';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UsersListComponent } from './user-details/users-list/users-list.component';
import { HomeComponent } from './home/home.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { LanguagesdetailsComponent } from './languagesdetails/languagesdetails.component';
import { InputFormComponent } from './user-details/input-form/input-form.component';
import { LanguageInputComponent } from './languagesdetails/language-input/language-input.component';
import { LanguageListComponent } from './languagesdetails/language-list/language-list.component';
import { TextEditorComponent } from './text-editor/text-editor.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LanguagesdetailsComponent,
    InputFormComponent,
    UserDetailsComponent,
    UsersListComponent,
    LanguageInputComponent,
    LanguageListComponent,
    TextEditorComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    QuillModule.forRoot(),
    RouterModule.forRoot(
      [
        { path: '', component: UserDetailsComponent, pathMatch: 'full' },
        { path: 'users', component: UsersListComponent },
        { path: 'languages', component: LanguagesdetailsComponent },
        { path: 'text-editor', component: TextEditorComponent },
      ],
      { relativeLinkResolution: 'legacy' },
    ),
    NgbModule,
    ReactiveFormsModule,
  ],
  providers: [{ provide: NgbDateAdapter, useClass: CustomNgbDateNativeUTCAdapter }],
  bootstrap: [AppComponent],
})
export class AppModule { }
