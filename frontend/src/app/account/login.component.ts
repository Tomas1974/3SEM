import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { firstValueFrom } from "rxjs";
import { ToastController } from "@ionic/angular";
import { TokenService } from "src/services/token.service";
import { Router } from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment.prod";
import { jwtDecode } from "jwt-decode";

export interface ResponseDto<T> {
  responseData: T;
}

@Component({
  template: `
      <app-title title="Login"></app-title>
      <ion-content>
          <form [formGroup]="form" (ngSubmit)="submit()">
              <ion-list>

                  <ion-item>
                      <ion-input formControlName="email" data-testid="emailInput" placeholder="name@company.com"
                                 label-placement="floating">
                          <div slot="label">Email
                              <ion-text *ngIf="email.touched && email.invalid"
                                        color="danger">Valid
                                  email is required
                              </ion-text>
                          </div>
                      </ion-input>
                  </ion-item>

                  <ion-item>
                      <ion-input type="password" formControlName="password" data-testid="passwordInput"
                                 placeholder="****************" label-placement="floating">
                          <div slot="label">Password
                              <ion-text
                                      *ngIf="password.touched && password.errors?.['required']"
                                      color="danger">
                                  Required
                              </ion-text>
                          </div>
                      </ion-input>
                  </ion-item>

              </ion-list>

              <ion-button id="btn-submit" [disabled]="form.invalid" (click)="submit()">Submit</ion-button>
              <ion-button id="btn-register" color="secondary" fill="outline" [routerLink]="'/register'">Register
              </ion-button>
          </form>
      </ion-content>
  `,
  styleUrls: ['./form.css'],
})
export class LoginComponent {
  readonly form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  get email() { return this.form.controls.email; }
  get password() { return this.form.controls.password; }

  constructor(
    private readonly fb: FormBuilder,
    private readonly http: HttpClient,
    private readonly router: Router,
    private readonly toast: ToastController,
    private readonly token: TokenService
  ) { }

  async submit() {
    if (this.form.invalid) return;
    const response = await firstValueFrom(this.http.post<ResponseDto<TokenService>>(environment.baseUrl + '/api/account/login', this.form.value));
    this.token.setToken(JSON.stringify(response.responseData));



    const encodedToken = JSON.stringify(sessionStorage.getItem("token"));
    const decoded = jwtDecode(encodedToken);

    console.log(decoded);

    /* prints:
     * {
     *   foo: "bar",
     *   exp: 1393286893,
     *   iat: 1393268893
     * }
     */

// decode header by passing in options (useful for when you need `kid` to verify a JWT):
    const decodedHeader = jwtDecode(encodedToken, { header: true });
    console.log(decodedHeader);





    (await this.toast.create({
      message: "Welcome back!",
      color: "success",
      duration: 5000
    })).present();
  }
}
