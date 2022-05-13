import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MerchantEventComponent } from './merchant-event.component';

describe('MerchantEventComponent', () => {
  let component: MerchantEventComponent;
  let fixture: ComponentFixture<MerchantEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MerchantEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MerchantEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
