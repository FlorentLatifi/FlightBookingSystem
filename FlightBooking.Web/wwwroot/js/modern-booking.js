// ============================================
// MODERN FLIGHT BOOKING - JAVASCRIPT
// ============================================

// System Insights Toggle
function toggleInsights() {
    const panel = document.getElementById('systemInsights');
    panel.classList.toggle('collapsed');
}

// Update System Insights
function updateInsights(data) {
    // Update Active Services
    const servicesContainer = document.getElementById('activeServices');
    if (servicesContainer && data.services) {
        servicesContainer.innerHTML = data.services.map(service => `
            <div class="insight-item">
                <div class="insight-icon layer-application">
                    <i class="bi bi-gear"></i>
                </div>
                <div class="insight-text">
                    <div class="insight-label">Service</div>
                    <div class="insight-value">${service}</div>
                </div>
            </div>
        `).join('');
    }

    // Update Active Patterns
    const patternsContainer = document.getElementById('activePatterns');
    if (patternsContainer && data.patterns) {
        patternsContainer.innerHTML = data.patterns.map(pattern => `
            <div class="insight-item">
                <div class="insight-icon" style="background: #fef3c7; color: #f59e0b;">
                    <i class="bi bi-lightning-fill"></i>
                </div>
                <div class="insight-text">
                    <div class="insight-label">Pattern</div>
                    <div class="insight-value">${pattern}</div>
                </div>
            </div>
        `).join('');
    }
}

// Show Notification (Observer Pattern Demo)
function showNotification(type, title, message) {
    const panel = document.getElementById('notificationPanel');
    if (!panel) return;

    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <div class="notification-icon">
            <i class="bi bi-${type === 'email' ? 'envelope-fill' : 'phone-fill'}"></i>
        </div>
        <div class="notification-content">
            <div class="notification-title">${title}</div>
            <div class="notification-message">${message}</div>
        </div>
    `;

    panel.appendChild(notification);

    // Auto-remove after 5 seconds
    setTimeout(() => {
        notification.style.animation = 'slideOut 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 5000);
}

// Show Loading Overlay
function showLoading(message = 'Duke procesuar...') {
    const overlay = document.createElement('div');
    overlay.id = 'loadingOverlay';
    overlay.className = 'loading-overlay';
    overlay.innerHTML = `
        <div class="loading-spinner">
            <div class="spinner"></div>
            <div class="loading-text">${message}</div>
        </div>
    `;
    document.body.appendChild(overlay);
}

// Hide Loading Overlay
function hideLoading() {
    const overlay = document.getElementById('loadingOverlay');
    if (overlay) {
        overlay.style.opacity = '0';
        setTimeout(() => overlay.remove(), 300);
    }
}

// Flight Card Selection
function selectFlight(flightId) {
    // Remove previous selection
    document.querySelectorAll('.flight-card').forEach(card => {
        card.classList.remove('selected');
    });

    // Add selection to clicked card
    const selectedCard = document.querySelector(`[data-flight-id="${flightId}"]`);
    if (selectedCard) {
        selectedCard.classList.add('selected');
    }

    // Update hidden input
    const hiddenInput = document.getElementById('selectedFlightId');
    if (hiddenInput) {
        hiddenInput.value = flightId;
    }

    // Update insights
    updateInsights({
        services: ['FlightService', 'ReservationService'],
        patterns: ['Repository Pattern', 'MVC Pattern']
    });
}

// Strategy Pattern Selector
function selectStrategy(strategy) {
    // Remove previous selection
    document.querySelectorAll('.strategy-option').forEach(option => {
        option.classList.remove('active');
    });

    // Add selection to clicked option
    const selectedOption = document.querySelector(`[data-strategy="${strategy}"]`);
    if (selectedOption) {
        selectedOption.classList.add('active');
    }

    // Update hidden input
    const hiddenInput = document.getElementById('selectedStrategy');
    if (hiddenInput) {
        hiddenInput.value = strategy;
    }

    // Update insights
    updateInsights({
        services: ['ReservationService', 'PricingStrategy'],
        patterns: ['Strategy Pattern', 'Dependency Injection']
    });

    // Show notification about strategy change
    const strategyNames = {
        'Standard': 'Standard Pricing',
        'Discount': 'Discount Pricing (10% OFF)',
        'Seasonal': 'Seasonal Pricing'
    };

    showNotification(
        'email',
        'Pricing Strategy Changed',
        `Pricing strategy switched to: ${strategyNames[strategy]}`
    );

    // Recalculate prices (if applicable)
    recalculatePrices(strategy);
}

// Recalculate Prices Based on Strategy
function recalculatePrices(strategy) {
    const basePriceElements = document.querySelectorAll('[data-base-price]');

    basePriceElements.forEach(element => {
        const basePrice = parseFloat(element.dataset.basePrice);
        let finalPrice = basePrice;

        switch (strategy) {
            case 'Standard':
                finalPrice = basePrice;
                break;
            case 'Discount':
                finalPrice = basePrice * 0.9; // 10% discount
                break;
            case 'Seasonal':
                const month = new Date().getMonth() + 1;
                // High season: June-August (6-8) and December (12)
                if ((month >= 6 && month <= 8) || month === 12) {
                    finalPrice = basePrice * 1.2; // 20% increase
                }
                break;
        }

        element.textContent = `€${finalPrice.toFixed(2)}`;
    });
}

// Form Validation Enhancement
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return true;

    const inputs = form.querySelectorAll('input[required], select[required]');
    let isValid = true;

    inputs.forEach(input => {
        if (!input.value) {
            isValid = false;
            input.classList.add('is-invalid');
        } else {
            input.classList.remove('is-invalid');
        }
    });

    return isValid;
}

// Smooth Scroll to Element
function scrollToElement(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
}

// Initialize on Page Load
document.addEventListener('DOMContentLoaded', function () {
    // Initialize insights with default data
    updateInsights({
        services: ['FlightService'],
        patterns: ['MVC Pattern', 'Repository Pattern']
    });

    // Add form validation listeners
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function (e) {
            if (!validateForm(form.id)) {
                e.preventDefault();
                scrollToElement(form.querySelector('.is-invalid').id);
            }
        });
    });

    // Initialize default strategy
    const defaultStrategy = document.querySelector('.strategy-option');
    if (defaultStrategy) {
        defaultStrategy.classList.add('active');
    }
});

// Observer Pattern Demo - Simulate Notifications
function demoObserverPattern(reservationCode) {
    // Simulate Email Notification
    setTimeout(() => {
        showNotification(
            'email',
            'Email Sent Successfully',
            `Confirmation email sent for reservation ${reservationCode}`
        );
    }, 500);

    // Simulate SMS Notification
    setTimeout(() => {
        showNotification(
            'sms',
            'SMS Sent Successfully',
            `Confirmation SMS sent for reservation ${reservationCode}`
        );
    }, 1000);

    // Update insights
    updateInsights({
        services: ['NotificationService', 'EmailService'],
        patterns: ['Observer Pattern', 'Parallel Processing']
    });
}

// Payment Processing Simulation
function processPayment(formData) {
    showLoading('Duke procesuar pagesën...');

    // Update insights
    updateInsights({
        services: ['PaymentService', 'ReservationService', 'NotificationService'],
        patterns: ['Strategy Pattern', 'Observer Pattern', 'Parallel Processing']
    });

    // Simulate payment processing (2 seconds)
    setTimeout(() => {
        hideLoading();

        // Simulate parallel processing notification
        showNotification(
            'email',
            'Payment Processing',
            'Payment and notification processing started in parallel...'
        );
    }, 2000);
}

// Wizard Navigation
function navigateWizard(step) {
    const steps = ['search', 'passengers', 'flights', 'payment', 'confirmation'];
    const currentIndex = steps.indexOf(step);

    // Update progress bar
    document.querySelectorAll('.progress-step').forEach((stepEl, index) => {
        if (index < currentIndex) {
            stepEl.classList.add('completed');
            stepEl.classList.remove('active');
        } else if (index === currentIndex) {
            stepEl.classList.add('active');
            stepEl.classList.remove('completed');
        } else {
            stepEl.classList.remove('active', 'completed');
        }
    });

    // Update insights based on current step
    const stepInsights = {
        'search': {
            services: ['FlightService'],
            patterns: ['Repository Pattern', 'MVC Pattern']
        },
        'passengers': {
            services: ['PassengerRepository'],
            patterns: ['DTO Pattern', 'Validation']
        },
        'flights': {
            services: ['FlightService', 'ReservationService'],
            patterns: ['Strategy Pattern', 'Repository Pattern']
        },
        'payment': {
            services: ['PaymentService', 'ReservationService'],
            patterns: ['Strategy Pattern', 'Observer Pattern']
        },
        'confirmation': {
            services: ['NotificationService', 'EmailService'],
            patterns: ['Observer Pattern', 'DTO Pattern']
        }
    };

    if (stepInsights[step]) {
        updateInsights(stepInsights[step]);
    }
}