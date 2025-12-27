// ============================================
// MODERN FLIGHT BOOKING - JAVASCRIPT
// Design Patterns Demo & UI Enhancement
// ============================================

// ============================================
// SYSTEM INSIGHTS PANEL
// ============================================

/**
 * Toggle System Insights Panel
 */
function toggleInsights() {
    const panel = document.getElementById('systemInsights');
    if (panel) {
        panel.classList.toggle('collapsed');

        // Update badge visibility
        const badge = document.getElementById('insightsBadge');
        if (badge) {
            badge.style.display = panel.classList.contains('collapsed') ? 'flex' : 'none';
        }
    }
}

/**
 * Update System Insights with current page data
 * @param {Object} data - Contains services and patterns arrays
 */
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

    // Update counts
    const patternCount = document.getElementById('patternCount');
    const serviceCount = document.getElementById('serviceCount');
    const badge = document.getElementById('insightsBadge');

    if (patternCount && data.patterns) {
        patternCount.textContent = data.patterns.length;
    }
    if (serviceCount && data.services) {
        serviceCount.textContent = data.services.length;
    }
    if (badge && data.patterns && data.services) {
        badge.textContent = data.patterns.length + data.services.length;
    }
}

// ============================================
// OBSERVER PATTERN - NOTIFICATIONS
// ============================================

/**
 * Show notification (Observer Pattern Demo)
 * @param {string} type - 'email' or 'sms'
 * @param {string} title - Notification title
 * @param {string} message - Notification message
 */
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
        <button class="notification-close" onclick="this.parentElement.remove()">
            <i class="bi bi-x-lg"></i>
        </button>
    `;

    panel.appendChild(notification);

    // Auto-remove after 5 seconds
    setTimeout(() => {
        notification.style.animation = 'slideOut 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 5000);
}

/**
 * Demo Observer Pattern with parallel notifications
 * @param {string} reservationCode - Reservation code
 */
function demoObserverPattern(reservationCode) {
    console.log('[Observer Pattern] Starting parallel notifications...');

    // Simulate Email Notification (Task 1)
    setTimeout(() => {
        showNotification(
            'email',
            'Email Sent Successfully',
            `Confirmation email sent for reservation ${reservationCode}`
        );
        console.log('[Observer Pattern] Email notification completed');
    }, 500);

    // Simulate SMS Notification (Task 2 - parallel with Email)
    setTimeout(() => {
        showNotification(
            'sms',
            'SMS Sent Successfully',
            `Confirmation SMS sent for reservation ${reservationCode}`
        );
        console.log('[Observer Pattern] SMS notification completed');
    }, 1000);

    // Update insights
    setTimeout(() => {
        updateInsights({
            services: ['NotificationService', 'EmailService', 'SmsService'],
            patterns: ['Observer Pattern', 'Parallel Processing']
        });
    }, 1500);
}

// ============================================
// LOADING STATES
// ============================================

/**
 * Show loading overlay
 * @param {string} message - Loading message
 */
function showLoading(message = 'Processing...') {
    // Remove existing overlay if any
    hideLoading();

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

/**
 * Hide loading overlay
 */
function hideLoading() {
    const overlay = document.getElementById('loadingOverlay');
    if (overlay) {
        overlay.style.opacity = '0';
        setTimeout(() => overlay.remove(), 300);
    }
}

// ============================================
// FLIGHT SELECTION
// ============================================

/**
 * Select a flight card
 * @param {number} flightId - Flight ID
 */
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

    // Update hidden input if exists
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

// ============================================
// STRATEGY PATTERN - PRICING
// ============================================

/**
 * Select pricing strategy (Strategy Pattern Demo)
 * @param {string} strategy - Strategy name ('Standard', 'Discount', 'Seasonal')
 */
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

    // Update hidden input if exists
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
        `Switched to: ${strategyNames[strategy]}`
    );

    // Recalculate prices
    recalculatePrices(strategy);
}

/**
 * Recalculate prices based on selected strategy
 * @param {string} strategy - Strategy name
 */
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

// ============================================
// WIZARD NAVIGATION
// ============================================

/**
 * Navigate wizard progress bar
 * @param {string} step - Current step ('search', 'flights', 'passengers', 'payment', 'confirmation')
 */
function navigateWizard(step) {
    const steps = ['search', 'flights', 'passengers', 'payment', 'confirmation'];
    const currentIndex = steps.indexOf(step);

    if (currentIndex === -1) return;

    // Update progress bar
    document.querySelectorAll('.progress-step').forEach((stepEl, index) => {
        stepEl.classList.remove('active', 'completed');
        if (index < currentIndex) {
            stepEl.classList.add('completed');
        } else if (index === currentIndex) {
            stepEl.classList.add('active');
        }
    });

    // Update insights based on current step
    const stepInsights = {
        'search': {
            services: ['FlightService'],
            patterns: ['Repository Pattern', 'MVC Pattern']
        },
        'flights': {
            services: ['FlightService', 'PricingStrategy'],
            patterns: ['Strategy Pattern', 'Repository Pattern']
        },
        'passengers': {
            services: ['PassengerRepository', 'ValidationService'],
            patterns: ['DTO Pattern', 'Validation']
        },
        'payment': {
            services: ['PaymentService', 'ReservationService', 'NotificationService'],
            patterns: ['Strategy Pattern', 'Observer Pattern']
        },
        'confirmation': {
            services: ['NotificationService', 'EmailService', 'SmsService'],
            patterns: ['Observer Pattern', 'Parallel Processing']
        }
    };

    if (stepInsights[step]) {
        updateInsights(stepInsights[step]);
    }
}

// ============================================
// FORM VALIDATION
// ============================================

/**
 * Validate form before submission
 * @param {string} formId - Form ID
 * @returns {boolean} - Is form valid
 */
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return true;

    const inputs = form.querySelectorAll('input[required], select[required]');
    let isValid = true;

    inputs.forEach(input => {
        if (!input.value || (input.type === 'email' && !isValidEmail(input.value))) {
            isValid = false;
            input.classList.add('is-invalid');

            // Show error message
            let errorMsg = input.parentElement.querySelector('.invalid-feedback');
            if (!errorMsg) {
                errorMsg = document.createElement('div');
                errorMsg.className = 'invalid-feedback';
                errorMsg.textContent = 'This field is required';
                input.parentElement.appendChild(errorMsg);
            }
        } else {
            input.classList.remove('is-invalid');
        }
    });

    return isValid;
}

/**
 * Validate email address
 * @param {string} email - Email address
 * @returns {boolean} - Is email valid
 */
function isValidEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

// ============================================
// SMOOTH SCROLL
// ============================================

/**
 * Smooth scroll to element
 * @param {string} elementId - Element ID
 */
function scrollToElement(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
}

// ============================================
// PAYMENT PROCESSING SIMULATION
// ============================================

/**
 * Process payment (Demo - shows parallel processing)
 * @param {Object} formData - Payment form data
 */
function processPayment(formData) {
    showLoading('Processing payment...');

    // Update insights
    updateInsights({
        services: ['PaymentService', 'ReservationService', 'NotificationService'],
        patterns: ['Strategy Pattern', 'Observer Pattern', 'Parallel Processing']
    });

    // Simulate payment processing (2 seconds)
    setTimeout(() => {
        hideLoading();

        // Show parallel processing notification
        showNotification(
            'email',
            'Payment Processing',
            'Payment and notification processing started in parallel...'
        );
    }, 2000);
}

// ============================================
// INITIALIZATION
// ============================================

/**
 * Initialize page when DOM is ready
 */
document.addEventListener('DOMContentLoaded', function () {
    console.log('[Flight Booking System] Initialized');

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
                const firstInvalid = form.querySelector('.is-invalid');
                if (firstInvalid) {
                    scrollToElement(firstInvalid.id);
                }
            }
        });
    });

    // Initialize default strategy
    const defaultStrategy = document.querySelector('.strategy-option');
    if (defaultStrategy) {
        defaultStrategy.classList.add('active');
    }

    // Add notification close button style
    addNotificationStyles();
});

/**
 * Add notification close button styles
 */
function addNotificationStyles() {
    const style = document.createElement('style');
    style.textContent = `
        .notification-close {
            background: none;
            border: none;
            color: #6b7280;
            cursor: pointer;
            padding: 0.5rem;
            margin-left: auto;
        }
        .notification-close:hover {
            color: #ef4444;
        }
    `;
    document.head.appendChild(style);
}

// ============================================
// CONSOLE WELCOME MESSAGE
// ============================================

console.log('%c🛫 Flight Booking System', 'color: #2563eb; font-size: 24px; font-weight: bold;');
console.log('%c🎯 Design Patterns: MVC, Repository, Strategy, Observer', 'color: #7c3aed; font-size: 14px;');
console.log('%c🏗️ Architecture: Onion/Clean Architecture', 'color: #10b981; font-size: 14px;');
console.log('%c📚 Academic Project - Design Patterns & Refactoring', 'color: #f59e0b; font-size: 14px;');