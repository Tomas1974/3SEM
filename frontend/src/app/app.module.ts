import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { ErrorHttpInterceptor } from 'src/interceptors/error-http-interceptor';
import { TokenService } from 'src/services/token.service';
import { AuthHttpInterceptor } from 'src/interceptors/auth-http-interceptor';
import { NgModule } from '@angular/core';
import { TabsComponent } from './tabs.component';
import { HeaderComponent } from './header.component';
import { RegisterComponent } from './account/register.component';
import { LoginComponent } from './account/login.component';
import { AccountComponent } from './account/account.component';
import { ProductsComponent } from './products/products.component';
import { UsersComponent } from './admin/users.component';
import { AuthenticatedGuard } from './guards';
import { AccountService } from './account/account.service';
import { ProductsService } from './products/products.service';
import {CartService} from "./cart/cart.service";
import {CartComponent} from "./cart/cart.component";
import {SearchComponent} from "./products/search.component";
import {CreateAvatarComponent} from "./products/createAvatar.component";
import {UpdateAvatarComponent} from "./products/updateAvatar.component";
import {DetailsAvatarComponent} from "./products/detailsAvatar.component";


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TabsComponent,
    ProductsComponent, //Jeg er her!
    UsersComponent,
    AccountComponent,
    RegisterComponent,
    LoginComponent,
    CartComponent,
    SearchComponent,
    CreateAvatarComponent,
    UpdateAvatarComponent,
    DetailsAvatarComponent
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot({ mode: 'ios' }),
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true },
    TokenService,
    AuthenticatedGuard,
    AccountService,
    ProductsService,
    CartService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
