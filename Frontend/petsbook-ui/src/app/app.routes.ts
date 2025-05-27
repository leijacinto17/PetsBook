import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegistrationComponent } from './features/auth/registration/registration.component';
import { SocialFeedComponent } from './features/social-feed/social-feed.component';
import { NotFoundComponent } from './features/not-found/not-found.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegistrationComponent },
    { path: 'social-feed', component: SocialFeedComponent },
    { path: '**', component: NotFoundComponent }
];
