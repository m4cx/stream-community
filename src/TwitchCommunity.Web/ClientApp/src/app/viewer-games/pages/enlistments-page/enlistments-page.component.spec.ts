import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { EnlistmentsPageComponent } from './enlistments-page.component';

describe('EnlistmentsPageComponent', () => {
  let component: EnlistmentsPageComponent;
  let fixture: ComponentFixture<EnlistmentsPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ EnlistmentsPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnlistmentsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
