import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbDateAdapter, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { InputFormComponent } from './user-details/input-form/input-form.component';
import { UsersListComponent } from './user-details/users-list/users-list.component';
import { CustomNgbDateNativeUTCAdapter } from './services';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserDetailsComponent,
    InputFormComponent,
    UsersListComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
], { relativeLinkResolution: 'legacy' }),
    NgbModule,
    ReactiveFormsModule,
  ],
  providers: [{ provide: NgbDateAdapter, useClass: CustomNgbDateNativeUTCAdapter }],
  bootstrap: [AppComponent],
})
export class AppModule {}
