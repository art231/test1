import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MobileStatisticsUpdateComponent } from './mobileStatistics-update.component';

describe('MobileStatisticsUpdateComponent', () => {
  let component: MobileStatisticsUpdateComponent;
  let fixture: ComponentFixture<MobileStatisticsUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MobileStatisticsUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MobileStatisticsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
